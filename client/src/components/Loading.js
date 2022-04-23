import {TailSpin} from "react-loader-spinner";

const Loading = () => {
  return (
    <TailSpin
      type="Puff"
      color="#00BFFF"
      height={100}
      width={100}
      timeout={3000} //3 secs
    />
  );
};
export default Loading;