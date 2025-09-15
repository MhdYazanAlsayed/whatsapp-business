import "../styles/loader.css";

interface Props {
  className?: string;
  size?: number;
}
const Loader = ({ className, size }: Props) => {
  return (
    <div
      className={`d-flex align-items-center justify-content-center ${className}`}
      style={{
        transform: size != undefined ? `scale(${size / 10})` : undefined,
      }}
    >
      <div className="loader-5 center">
        <span></span>
      </div>
    </div>
  );
};

export default Loader;
