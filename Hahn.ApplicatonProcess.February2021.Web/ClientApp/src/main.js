import { __awaiter } from "tslib";
import { PLATFORM } from 'aurelia-framework';
import { TCustomAttribute } from 'aurelia-i18n';
import Backend from 'i18next-xhr-backend';
import 'bootstrap';
import 'bootstrap-datepicker';
import 'aurelia-validation';
export function configure(au) {
    return __awaiter(this, void 0, void 0, function* () {
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
        yield au.start();
        yield au.setRoot(PLATFORM.moduleName('app'));
    });
}
//# sourceMappingURL=main.js.map