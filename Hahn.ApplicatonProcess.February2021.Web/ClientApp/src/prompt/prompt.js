import { __decorate, __metadata } from "tslib";
import { DialogController } from 'aurelia-dialog';
import { bindable, inject } from 'aurelia-framework';
7;
export var DialogStyle;
(function (DialogStyle) {
    DialogStyle[DialogStyle["YesNo"] = 0] = "YesNo";
    DialogStyle[DialogStyle["Ok"] = 1] = "Ok";
})(DialogStyle || (DialogStyle = {}));
export class DialogModel {
}
let Prompt = class Prompt {
    constructor(dialogController) {
        this.dialogController = dialogController;
    }
    activate(config) {
        this.config = config;
    }
    get isYesNoDialog() {
        return this.config.dialogStyle == DialogStyle.YesNo;
    }
    get isOkDialog() {
        return this.config.dialogStyle == DialogStyle.Ok;
    }
    Ok() {
        this.dialogController.ok(this.config);
    }
    Cancel() {
        this.dialogController.cancel(true);
    }
};
__decorate([
    bindable,
    __metadata("design:type", DialogModel)
], Prompt.prototype, "config", void 0);
Prompt = __decorate([
    inject(DialogController),
    __metadata("design:paramtypes", [DialogController])
], Prompt);
export { Prompt };
//# sourceMappingURL=prompt.js.map