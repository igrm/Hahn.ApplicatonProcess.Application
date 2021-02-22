import { Aurelia, PLATFORM } from 'aurelia-framework';
import { I18N, TCustomAttribute } from 'aurelia-i18n';
import Backend from 'i18next-http-backend';

import 'bootstrap';
import 'bootstrap-datepicker';
import 'aurelia-validation';




export async function configure(au: Aurelia) {
    au.use.standardConfiguration();
    au.use.developmentLogging();
    au.use.plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
        let aliases = ['t', 'i18n'];
        TCustomAttribute.configureAliases(aliases);

        instance.i18next.use(Backend);

        return instance.setup({
            backend: {                                  
                loadPath: './locales/{{lng}}/{{ns}}.json',
            },
            attributes: aliases,
            lng: 'en',
            fallbackLng: false,
            debug: false
        });
    });

    au.use.plugin(PLATFORM.moduleName('aurelia-validation'));
    au.use.plugin(PLATFORM.moduleName('aurelia-dialog'));

    await au.start();
    await au.setRoot(PLATFORM.moduleName('app'));
}
