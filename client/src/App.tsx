import { useEffect, useState } from 'react';
import type { Activity } from './lib/types';


const App = () => {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    fetch('https://localhost:5001/api/activities')
      .then((response) => response.json())
      .then((data) => setActivities(data));
  }, []);

  return (
    <>
      <h3>Adirs Evvvents 🥳</h3>
      <ul>
        {activities.map((activity) => (
          <li key={activity.id}>{activity.title}</li>
        ))}
      </ul>
    </>
  );
};

export default App;
