import { Languages } from "../enums/Languages";

export interface ILanguage {
  resources: any | null;
  isRTL: boolean | null;

  changeLanguage(lang: Languages): void;
  load(): void;
  setLanguage(resources: any, isRTL: boolean): void;
  configure(reRenderMethod: any): void;
}
