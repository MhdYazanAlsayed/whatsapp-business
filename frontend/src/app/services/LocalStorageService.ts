import ILocalStorage from "../core/contracts/ILocalStorage";

export default class LocalStorageService implements ILocalStorage {
  private readonly _prefix = "sprint-business-";

  getItem<T>(key: string): T | null {
    const item = localStorage.getItem(this._prefix + key);
    try {
      return JSON.parse(item as any) as T | null;
    } catch {
      return item as T | null;
    }
  }
  setItem<T>(key: string, data: T): void {
    localStorage.setItem(this._prefix + key, JSON.stringify(data));
  }
  removeItem(key: string): void {
    localStorage.removeItem(this._prefix + key);
  }
}
