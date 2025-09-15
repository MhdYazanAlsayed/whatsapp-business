import ServiceProvider from "./ServiceProvider";

export default class DependenciesInjector {
  static services: ServiceProvider = new ServiceProvider();
}
