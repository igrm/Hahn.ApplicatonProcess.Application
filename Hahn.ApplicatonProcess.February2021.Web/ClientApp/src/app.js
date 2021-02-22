import { __decorate, __metadata } from "tslib";
import { PLATFORM, inject } from "aurelia-framework";
import { I18N } from 'aurelia-i18n';
import { ValidationMessageProvider } from 'aurelia-validation';
import { SettingsService } from './services/settings-service';
let App = class App {
    constructor(i18n, settingsService) {
        this.i18n = i18n;
        this.settingsService = settingsService;
        this.i18n.setLocale(settingsService.defaultLocale);
        ValidationMessageProvider.prototype.getMessage = function (key) {
            const translation = i18n.tr(`errorMessages.${key}`);
            return this.parser.parse(translation);
        };
        ValidationMessageProvider.prototype.getDisplayName = function (propertyName, displayName) {
            return i18n.tr(displayName ? displayName : propertyName);
        };
    }
    configureRouter(config, router) {
        this.router = router;
        config.map([
            { route: ['', 'input'], name: 'assetinput', title: this.i18n.tr("keyTitle"), moduleId: PLATFORM.moduleName('assetinput/assetinput'), nav: true },
            { route: ['confirm'], name: 'confirm', title: this.i18n.tr("keyThankYou"), moduleId: PLATFORM.moduleName('confirm/confirm'), nav: true },
        ]);
    }
};
App = __decorate([
    inject(I18N, SettingsService),
    __metadata("design:paramtypes", [Object, Object])
], App);
export { App };
//# sourceMappingURL=app.js.map