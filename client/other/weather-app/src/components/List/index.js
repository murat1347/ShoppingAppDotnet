import { useWeather } from '../../context/WeatherContext';
import ListItem from './ListItem';

function List() {
  const { weatherData } = useWeather();

  return (
    <div className='list'>
      {weatherData.map((day) => (
        <ListItem
          key={day.dt}
          max={day.temp.max}
          min={day.temp.min}
          icon={day.weather[0].icon}
          dt={day.dt}
        />
      ))}
    </div>
  );
}

export default List;
