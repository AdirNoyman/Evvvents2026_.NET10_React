import { useEffect, useState } from 'react';
import type { Activity } from './lib/types';
import { List, ListItem, ListItemText, Typography } from '@mui/material';
import axios from 'axios';



const App = () => {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
   axios<Activity[]>('https://localhost:5001/api/activities').then(res => setActivities(res.data));

  }, []);

  return (
    <>
      <Typography variant="h3">Adirs Evvvents 🥳</Typography>
      <List>
        {activities.map((activity) => (
          <ListItem key={activity.id}><ListItemText>{activity.title}</ListItemText></ListItem>
        ))}
      </List>
    </>
  );
};

export default App;
