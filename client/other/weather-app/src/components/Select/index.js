import axios from 'axios';

import { useWeather } from '../../context/WeatherContext.js';

import config from '../../config.json';
import { data } from '../../service.js';

function Select() {
  const { setWeatherData } = useWeather();

  const handleChange = (e) => {
    const city = data.find((el) => el.id === parseInt(e.target.value));

    axios(
      `https://api.openweathermap.org/data/2.5/onecall?lat=${city.lat}&lon=${city.lon}&appid=${config.API_KEY}&units=metric&lang=tr`
    ).then((data) => setWeatherData(data.data.daily));
  };

  return (
    <select className='select' defaultValue='34' onChange={handleChange}>
      {data.map((el) => (
        <option value={el.id} key={el.id}>
          {el.name}
        </option>
      ))}
    </select>
  );
}

export default Select;
