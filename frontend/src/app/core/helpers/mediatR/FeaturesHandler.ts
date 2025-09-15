import DependenciesInjector from "../../util/DependenciesInjector";
import Feature from "./Feature";
import IRequest from "./IRequest";

export default class FeaturesHandler {
  private features: Feature[] = [];

  add(command: any, handler: any): void {
    this.features.push({ command: command.name, handler: handler });
  }

  async executeAsync<T>(command: IRequest<T>): Promise<T> {
    const commandName = command.constructor.name;

    const item = this.features.find((x) => x.command == commandName);
    if (!item) {
      throw new Error("Cannot find handler ..");
    }

    return await new item.handler(DependenciesInjector.services).HandleAsync(
      command
    );
  }
}
