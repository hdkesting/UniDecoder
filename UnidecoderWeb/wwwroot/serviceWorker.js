var cache;

self.addEventListener('install', event => {
    console.log("getting current version");

    event.waitUntil(
        fetch("api/unicode/version").then(resp => {
            console.log("got response");
            resp.json().then(v => {
                console.log("using version " + v);
                caches.open('uc' + v).then(c => {
                    cache = c;
                    cache.put('api/unicode/characters');
                    cache.put('api/unicode/blocks');
                });
            });
        })
    );
});

self.addEventListener('activate', event => {
    console.log('Ready to handle fetches!');
});

self.addEventListener('fetch', event => {
    console.log("fetch event...");
    event.respondWith(cache.match(event.request)
        .then(res => {
            if (res) {
                return res;
            }

            console.log("fetching...");
            return fetch(event.request).then(resp => {
                cache.put(event.request, resp.clone());
                return res;
            });
        }));
});
