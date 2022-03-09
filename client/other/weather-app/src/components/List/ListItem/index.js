import React from 'react';

function Image({ max, min, icon, dt }) {
  const days = ['Sun', 'Mon', 'Tues', 'Wed', 'Thurs', 'Fri', 'Sat'];
  const day = days[new Date(dt * 1000).getDay()];
  const active =
    new Date(dt * 1000).getDate() === new Date().getDate() ? true : false;

  return (
    <div className={`listItem ${active ? 'active' : null}`}>
      <span>{day}</span>
      <img
        alt='dayWeatherImage'
        src={`http://openweathermap.org/img/wn/${icon}.png`}
      />
      <div>
        <span>{parseInt(max)}°</span>
        <span>{parseInt(min)}°</span>
      </div>
    </div>
  );
}

export default Image;
