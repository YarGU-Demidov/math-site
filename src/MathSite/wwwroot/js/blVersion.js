Object.defineProperty(window, 'blVersion',
    {
        get: function() {
            var metas = document.querySelectorAll('meta');
            var version = 'none';

            metas.forEach(it => {
                if (it.name === 'site-version') {
                    version = it.content;
                }
            });
                
            return version;
        }
    })