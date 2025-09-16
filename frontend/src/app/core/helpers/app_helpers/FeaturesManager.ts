import { Features } from "src/app/features/Features";
import App from "./App";

export default class FeaturesManager {
  constructor() {}

  inject() {
    App.features.addAll(
      new Map(
        Features.map((feature) => [
          feature.command.name,
          { handler: feature.handler },
        ])
      )
    );
  }
}
