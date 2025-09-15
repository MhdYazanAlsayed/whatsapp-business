const Footer = ({
  footerComponent,
  setFooterComponent,
}: {
  footerComponent: string;
  setFooterComponent: (value: string) => void;
}) => {
  return (
    <div className="mb-3">
      <h5 className="py-3 mb-3 text-blue fw-semibold">التذييل</h5>

      <div className="col-md-12 mb-3">
        <div className="form-group">
          <label htmlFor="footer" className="mb-1 text-muted fw-semibold">
            النص
          </label>

          <input
            type="text"
            className="form-control"
            id="footer"
            value={footerComponent}
            onChange={(e) => setFooterComponent(e.target.value)}
          />
        </div>
      </div>
    </div>
  );
};

export default Footer;
