import axios from 'axios';

import { createContext, useState, useContext, useEffect } from 'react';

import config from '../config.json';
import { data } from '../service';

const WeatherContext = createContext();

export const WeatherProvider = ({ children }) => {
  const [weatherData, setWeatherData] = useState([]);

  useEffect(() => {
    const city = data.find((el) => el.id === 34);

    axios(
      `https://api.openweathermap.org/data/2.5/onecall?lat=${city.lat}&lon=${city.lon}&appid=${config.API_KEY}&units=metric&lang=tr`
    ).then((data) => setWeatherData(data.data.daily));
  }, []);

  const values = {
    weatherData,
    setWeatherData,
  };
  return (
    <WeatherContext.Provider value={values}>{children}</WeatherContext.Provider>
  );
};

export const useWeather = () => useContext(WeatherContext);
