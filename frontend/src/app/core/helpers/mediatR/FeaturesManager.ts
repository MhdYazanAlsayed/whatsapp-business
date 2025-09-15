import { Features } from "src/app/features/Features";
import MediatR from "./MediatR";

export default class FeaturesManager {
  constructor() {}

  inject() {
    for (let x of Features) {
      MediatR.features.add(x.command, x.handler);
    }
  }
}
