export default interface ILocalStorage {
  getItem<T>(key: string): T | null;
  setItem<T>(key: string, data: T): void;
  removeItem(key: string): void;
}
