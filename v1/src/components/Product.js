import React, { useState } from "react";
import { addToChart } from "../redux/actions/chartActions";
import { useDispatch } from "react-redux";
import { Link } from "react-router-dom";
import { Image } from "@chakra-ui/react";


const Phone = ({ phone }) => {
  const dispatch = useDispatch();
  const [isHover, setIsHover] = useState(false);
  const unVisibleButton = { visibility: "hidden" };
  return (
    <div className="col mt-5">
      <div
        className="card p-2"
        onMouseEnter={() => setIsHover(true)}
        onMouseLeave={() => setIsHover(false)}
      >
        <Link to={{ pathname: `/`, phone: phone }}>
        <Image src={phone.imageUrl} alt="product" loading="lazy" />
        </Link>
        <div className="card-body">
          <Link
            to={{ pathname: `/phone-detail/${phone.id}`, phone: phone }}
            style={{
              textDecoration: "none",
              fontSize: "0.7rem",
            }}
          >
            {phone.name}
          </Link>
          <h4>${phone.price.toFixed(2)}</h4>
          <p
            className="card-text"
            style={{ fontSize: "0.7rem", marginBottom: "3rem" }}
          >
          </p>
          <div style={{ display: "flex", justifyContent: "center" }}>
            <button
              className="btn btn-primary "
              style={isHover ? null : unVisibleButton}
              onClick={() =>
                dispatch((dispatch) => addToChart(dispatch, phone))
              }
            >
              Add to Chart
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Phone;
