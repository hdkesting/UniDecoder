// for unloading and to view logging: chrome://serviceworker-internals/
// source: https://developers.google.com/web/fundamentals/primers/service-workers/

var CURRENT_CACHES = {
    prefetch: "unicode-decoder-v10"
};

var urlsToPrefetch = [
    "index.html",
    "css/site.min.css",
    "js/handlebars.min.js",
    "js/process.js",
    "api/unicode/characters",
    "api/unicode/blocks"
];

// https://googlechrome.github.io/samples/service-worker/window-caches/

self.addEventListener('install', function (event) {
    // Perform install steps
    console.log("installing SW with cache " + CURRENT_CACHES.prefetch);
    event.waitUntil(
        caches.open(CURRENT_CACHES.prefetch).then(function (cache) {
            return cache.addAll(urlsToPrefetch).then(function () {
                console.log('All resources have been fetched and cached.');
                // skipWaiting() allows this service worker to become active
                // immediately, bypassing the waiting state, even if there's a previous
                // version of the service worker already installed.
                self.skipWaiting();
            });
        }).catch(function (error) {
            // This catch() will handle any exceptions from the caches.open()/cache.addAll() steps.
            console.error('Pre-fetching failed:', error);
        })
    );
});

self.addEventListener('activate', function (event) {
    // clients.claim() tells the active service worker to take immediate
    // control of all of the clients under its scope.
    self.clients.claim();

    // Delete all caches that aren't named in CURRENT_CACHES.
    // While there is only one cache in this example, the same logic will handle the case where
    // there are multiple versioned caches.
    var expectedCacheNames = Object.keys(CURRENT_CACHES).map(function (key) {
        return CURRENT_CACHES[key];
    });

    event.waitUntil(
        caches.keys().then(function (cacheNames) {
            return Promise.all(
                cacheNames.map(function (cacheName) {
                    if (expectedCacheNames.indexOf(cacheName) === -1) {
                        // If this cache name isn't present in the array of "expected" cache names,
                        // then delete it.
                        console.log('Deleting out of date cache:', cacheName);
                        return caches.delete(cacheName);
                    }
                })
            );
        })
    );
});

self.addEventListener('fetch', function (event) {
    event.respondWith(
        self.caches.match(event.request)
            .then(function (response) {
                // Cache hit - return response
                if (response) {
                    console.log("responded from cache");
                    return response;
                }

                // IMPORTANT: Clone the request. A request is a stream and
                // can only be consumed once. Since we are consuming this
                // once by cache and once by the browser for fetch, we need
                // to clone the response.
                var fetchRequest = event.request.clone();
                console.log("fetching it");
                return fetch(fetchRequest).then(
                    function (response) {
                        // Check if we received a valid response
                        if (!response || response.status !== 200 || response.type !== 'basic') {
                            return response;
                        }

                        // IMPORTANT: Clone the response. A response is a stream
                        // and because we want the browser to consume the response
                        // as well as the cache consuming the response, we need
                        // to clone it so we have two streams.
                        var responseToCache = response.clone();

                        caches.open(CACHE_NAME)
                            .then(function (cache) {
                                cache.put(event.request, responseToCache);
                            });

                        return response;
                    }
                );
            })
    );
});
