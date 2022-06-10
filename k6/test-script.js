import http from 'k6/http';
import { sleep } from 'k6';
export const options = {
  vus: 1000,
  duration: '5s',
};
var counter = 2000;
export default function () {
  http.post(
    // 'http://localhost:8070/WeatherForecast/Insert',
    'http://posts.com/WeatherForecast/Insert',
    JSON.stringify({
      name: 'string ' + counter,
      model: counter++,
    }),
    {
      headers: {
        'Content-Type': 'application/json',
      },
    }
  );
  // sleep(1);
}
