export interface IHttp {
  readonly _api: string;
  postData<T>(url: string, data?: T): Promise<Response>;
  patchData<T>(url: string, data?: T): Promise<Response>;
  putData<T>(url: string, data?: T): Promise<Response>;
  postDataWithFile(url: string, data: FormData): Promise<Response>;
  putDataWithFile(url: string, data: FormData): Promise<Response>;
  getData(url: string): Promise<Response>;
  deleteData<T>(url: string, data?: T): Promise<Response>;
}
