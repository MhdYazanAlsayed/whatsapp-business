import { ILanguage } from "../core/contracts/ILanguage";
import ILocalStorage from "../core/contracts/ILocalStorage";
import { Languages } from "../core/enums/Languages";
import Arabic from "../core/resources/arabic.json";
import English from "../core/resources/english.json";

export default class LanguageService implements ILanguage {
  resources: any | null;
  isRTL: boolean | null;
  reRenderMethod: any;

  constructor(readonly _localStorage: ILocalStorage) {
    this.resources = null;
    this.isRTL = null;
    this.reRenderMethod = null;
  }

  changeLanguage(lang: Languages): void {
    const isRTL = lang === Languages.Arabic;

    this.setLanguage(isRTL ? Arabic : English, isRTL);
    this._localStorage.setItem("language", lang);

    // Triger event to refresh compnents
    if (this.reRenderMethod) this.reRenderMethod();
  }

  setLanguage(resources: any, isRTL: boolean): void {
    this.resources = resources;
    this.isRTL = isRTL;
  }

  load(): void {
    const data = this._localStorage.getItem<Languages>("language");
    if (!data) {
      this.setLanguage(Arabic, true);
      return;
    }

    const isRTL = data === Languages.Arabic;
    this.setLanguage(isRTL ? Arabic : English, isRTL);
  }

  configure(reRenderMethod: any): void {
    this.reRenderMethod = reRenderMethod;
  }
}
