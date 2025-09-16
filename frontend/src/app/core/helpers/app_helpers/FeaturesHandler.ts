import DependenciesInjector from "../../util/DependenciesInjector";
import IRequest from "./IRequest";
import ServiceProvider from "../../util/ServiceProvider";
import { IRequestHandler } from "./IRequestHandler";

type FeatureEntry = {
  handler: new (services: ServiceProvider) => IRequestHandler<any, any>;
};

export default class FeaturesHandler {
  private features = new Map<string, FeatureEntry>();

  add(
    commandType: string,
    handler: new (services: ServiceProvider) => IRequestHandler<any, any>
  ): void {
    this.features.set(commandType, { handler });
  }

  addAll(entries: Map<string, FeatureEntry>) {
    this.features = entries;
  }

  async executeAsync<T>(command: IRequest<T>): Promise<T> {
    const commandName = command.constructor.name;

    const item = this.features.get(commandName);
    if (!item) {
      throw new Error("Cannot find handler ..");
    }

    return await new item.handler(DependenciesInjector.services).handleAsync(
      command
    );
  }
}
