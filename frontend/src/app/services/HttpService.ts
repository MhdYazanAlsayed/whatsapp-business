import { IHttp } from "../core/contracts/IHttp";

import Settings from "../core/appsettings.json";
import { IHostEnviroment } from "../core/contracts/IHostEnviroment";
import { ILogger } from "../core/contracts/ILogger";
import { ILanguage } from "../core/contracts/ILanguage";
import { toast } from "react-toastify";

export default class HttpService implements IHttp {
  public readonly _api = Settings.Api.Development;

  constructor(
    private readonly _logger: ILogger,
    private readonly _language: ILanguage,
    private readonly _hostEnviroment: IHostEnviroment
  ) {
    this._api = this._hostEnviroment.isDevelopment()
      ? Settings.Api.Development
      : Settings.Api.Production;
  }

  async postData<T>(url: string, data?: T) {
    try {
      let response = await fetch(this._api + url, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "include", // include, *same-origin, omit
        headers: {
          "Content-Type": "application/json",
          language: this._language.isRTL ? "ar" : "en",
          // 'Content-Type': 'application/x-www-form-urlencoded',
          // Authorization:
          //   this._authorization.accessToken != null
          //     ? `Bearer ${this._authorization.accessToken}`
          //     : "",
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: data ? JSON.stringify(data) : null, // body data type must match "Content-Type" header
      });

      this.logByStatus(response, url, "POST");

      return response;
    } catch (err) {
      this._logger.logError(`error [${url}]: ${err}`);

      toast.error(
        "التطبيق لا يعمل .. الرجاء التواصل مع فريق الدعم لحل المشكلة ."
      );

      throw new Error(
        "There is an error that happend while tring to connect to the server ."
      );
    }
  }

  async patchData<T>(url: string, data?: T) {
    try {
      let response = await fetch(this._api + url, {
        method: "PATCH", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "include", // include, *same-origin, omit
        headers: {
          "Content-Type": "application/json",
          language: this._language.isRTL ? "ar" : "en",
          // 'Content-Type': 'application/x-www-form-urlencoded',
          // Authorization: this._authorization.accessToken
          //   ? `Bearer ${this._authorization.accessToken}`
          //   : "",
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: data ? JSON.stringify(data) : null, // body data type must match "Content-Type" header
      });

      this.logByStatus(response, url, "PATCH");

      return response;
    } catch (err) {
      this._logger.logError(`error [${url}]: ${err}`);

      toast.error(
        "التطبيق لا يعمل .. الرجاء التواصل مع فريق الدعم لحل المشكلة ."
      );

      throw new Error(
        "There is an error that happend while tring to connect to the server ."
      );
    }
  }

  async putData<T>(url: string, data?: T): Promise<Response> {
    try {
      let response = await fetch(this._api + url, {
        method: "PUT", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "include", // include, *same-origin, omit
        headers: {
          "Content-Type": "application/json",
          language: this._language.isRTL ? "ar" : "en",
          // 'Content-Type': 'application/x-www-form-urlencoded',
          // Authorization: this._authorization.accessToken
          //   ? `Bearer ${this._authorization.accessToken}`
          //   : "",
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: data === null ? null : JSON.stringify(data), // body data type must match "Content-Type" header
      });

      this.logByStatus(response, url, "PUT");

      return response;
    } catch (err) {
      this._logger.logError(`error [${url}]: ${err}`);

      toast.error(
        "التطبيق لا يعمل .. الرجاء التواصل مع فريق الدعم لحل المشكلة ."
      );

      throw new Error(
        "There is an error that happend while tring to connect to the server ."
      );
    }
  }

  async postDataWithFile(url: string, data: FormData) {
    try {
      let response = await fetch(this._api + url, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "include", // include, *same-origin, omit
        headers: {
          language: this._language.isRTL ? "ar" : "en",
          // Authorization:
          //   this._authorization.accessToken != null
          //     ? `Bearer ${this._authorization.accessToken}`
          //     : "",
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: data, // body data type must match "Content-Type" header
      });

      this.logByStatus(response, url, "POST");

      return response;
    } catch (err) {
      this._logger.logError(`error [${url}]: ${err}`);

      toast.error(
        "التطبيق لا يعمل .. الرجاء التواصل مع فريق الدعم لحل المشكلة ."
      );

      throw new Error(
        "There is an error that happend while tring to connect to the server ."
      );
    }
  }

  async putDataWithFile(url: string, data: FormData) {
    try {
      let response = await fetch(this._api + url, {
        method: "PUT", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "include", // include, *same-origin, omit
        headers: {
          language: this._language.isRTL ? "ar" : "en",
          // Authorization:
          //   this._authorization.accessToken != null
          //     ? `Bearer ${this._authorization.accessToken}`
          //     : "",
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: data, // body data type must match "Content-Type" header
      });

      this.logByStatus(response, url, "POST");

      return response;
    } catch (err) {
      this._logger.logError(`error [${url}]: ${err}`);

      toast.error(
        "التطبيق لا يعمل .. الرجاء التواصل مع فريق الدعم لحل المشكلة ."
      );

      throw new Error(
        "There is an error that happend while tring to connect to the server ."
      );
    }
  }

  async getData(url: string): Promise<Response> {
    try {
      let response = await fetch(this._api + url, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          language: this._language.isRTL ? "ar" : "en",
          // Authorization: this._authorization.accessToken
          //   ? `Bearer ${this._authorization.accessToken}`
          //   : "",
        },
        credentials: "include",
      });

      this.logByStatus(response, url, "GET");

      return response;
    } catch (err) {
      this._logger.logError(`error [${url}]: ${err}`);

      toast.error(
        "التطبيق لا يعمل .. الرجاء التواصل مع فريق الدعم لحل المشكلة ."
      );

      throw new Error(
        "There is an error that happend while tring to connect to the server ."
      );
    }
  }

  async deleteData<T>(url: string, data?: T) {
    try {
      let response = await fetch(this._api + url, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          language: this._language.isRTL ? "ar" : "en",
          // Authorization:
          //   this._authorization.accessToken != null
          //     ? `Bearer ${this._authorization.accessToken}`
          //     : "",
        },
        credentials: "include",
        body: !data ? null : JSON.stringify(data),
      });

      this.logByStatus(response, url, "DELETE");

      return response;
    } catch (err) {
      this._logger.logError(`error [${url}]: ${err}`);

      toast.error(
        "التطبيق لا يعمل .. الرجاء التواصل مع فريق الدعم لحل المشكلة ."
      );

      throw new Error(
        "There is an error that happend while tring to connect to the server ."
      );
    }
  }

  private logByStatus(response: Response, url: string, verb: string) {
    if (response.status === 200)
      this._logger.logInformation(`${verb} ${response.status} [${url}]`);
    else this._logger.logError(`${verb} ${response.status} [${url}]`);
  }
}
