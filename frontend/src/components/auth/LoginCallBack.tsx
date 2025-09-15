import { useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import MediatR from "src/app/core/helpers/mediatR/MediatR";
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

    await MediatR.features.executeAsync(
      new ValidateCodeCommand({
        code: code,
      })
    );

    navigate("/");
  };

  return null;
};

export default LoginCallBack;
