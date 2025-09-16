import { useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import App from "src/app/core/helpers/app_helpers/App";
import ValidateCodeCommand from "src/app/features/account/commands/ValidateCode/ValidateCodeCommand";
import { useQuery } from "src/hooks/useQuery";

const LoginCallBack = () => {
  const { search } = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    handleValidateCodeAsync();
  }, []);

  const handleValidateCodeAsync = async () => {
    const { code } = useQuery(search);
    if (!code) {
      navigate("/login");
      return;
    }

    await App.features.executeAsync(
      new ValidateCodeCommand({
        code: code,
      })
    );

    navigate("/");
  };

  return null;
};

export default LoginCallBack;
