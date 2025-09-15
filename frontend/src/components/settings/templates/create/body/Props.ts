import { FormData } from "../Props";

export default interface Props {
  direction: string;
  bodyComponent: FormData;
  setBodyComponent: (
    bodyComponent: FormData | ((prev: FormData) => FormData)
  ) => void;
}
