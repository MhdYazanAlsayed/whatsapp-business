import { useEffect, useState } from "react";
import MediatR from "src/app/core/helpers/mediatR/MediatR";
import GetAccountStatusCommand from "src/app/features/account/queries/GetAccountStatus/GetAccountStatusCommand";
import ChangeEmployeeStatusCommand from "src/app/features/employee/commands/change-status/ChangeEmployeeStatusCommand";

const AccountActiveStatus = () => {
  const [active, setActive] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    handleGetAccountStatusAsync();
  }, []);

  const handleGetAccountStatusAsync = async () => {
    const result = await MediatR.features.executeAsync(
      new GetAccountStatusCommand()
    );

    setActive(result.succeeded);
    setLoading(false);
  };

  const handleSwitchStatusAsync = () => {
    setActive((x) => {
      let newValue = !x;

      if (newValue) {
        MediatR.features.executeAsync(
          new ChangeEmployeeStatusCommand({
            isActive: newValue,
          })
        );
      } else {
        MediatR.features.executeAsync(
          new ChangeEmployeeStatusCommand({
            isActive: newValue,
          })
        );
      }

      return newValue;
    });
  };

  return (
    <div>
      <hr className="text-white mt-auto" />
      <small className="fw-semibold text-white">قم بتفعيل نظام المراقبة</small>
      {loading ? (
        <small className="text-white fw-semibold text-center py-2 d-block">
          جاري التحميل ..
        </small>
      ) : (
        <div
          data-status={active ? "on" : "off"}
          className="switch"
          onClick={handleSwitchStatusAsync}
        >
          <small className="on">ON</small>
          <small className="off">OFF</small>
        </div>
      )}
    </div>
  );
};

export default AccountActiveStatus;
