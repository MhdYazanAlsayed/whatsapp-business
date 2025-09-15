import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import UserImage from "src/assets/images/user.png";

const _authenticator = DependenciesInjector.services.authenticator;

const ProfileInformation = () => {
  return (
    <div className="d-flex align-items-center gap-2">
      <div className="user-img">
        <img src={UserImage} alt="" />
      </div>
      <div>
        <p className="user-name">
          ممثل خدمة العملاء : {_authenticator.identity?.englishName}
        </p>
        <small className="text-muted">{_authenticator.identity?.email}</small>
      </div>
    </div>
  );
};

export default ProfileInformation;
