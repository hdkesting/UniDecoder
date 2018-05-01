# Unidecoder web-version

A SPA to explore the unicode character set.

## Security

As a test, an "integrity" attibute is added to the script references. So when the scripts change, update that value, else the scripts will not be loaded (in modern browsers).

For "handlebars.min.js" *two* values are required. The second one is for "handlebars.js", which is returned in debug mode.
