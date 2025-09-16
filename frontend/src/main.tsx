import { createRoot } from "react-dom/client";
import App from "./components/App.tsx";
import "./components/styles/master.css";
import FeaturesManager from "./app/core/helpers/app_helpers/FeaturesManager.ts";

// Inject all features
new FeaturesManager().inject();

createRoot(document.getElementById("root")!).render(<App />);
