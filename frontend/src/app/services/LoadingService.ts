import { ILoading } from "../core/contracts/ILoading";

export default class LoadingService implements ILoading {
  setLoading: (value: boolean) => void;

  constructor() {
    this.setLoading = () => {
      // console.log("Faild loading : " + value);
    };
  }
}
