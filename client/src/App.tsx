import { useEffect, useState } from 'react';
import type { Activity } from './lib/types';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import axios from 'axios';



const App = () => {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
   axios<Activity[]>('https://localhost:5001/api/activities').then(res => setActivities(res.data));

  }, []);

  return (
    <div className="container mx-auto p-6">
      <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl mb-8">
        Adirs Evvvents 🥳
      </h1>
      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
        {activities.map((activity) => (
          <Card key={activity.id}>
            <CardHeader>
              <CardTitle>{activity.title}</CardTitle>
            </CardHeader>
            <CardContent>
              <p className="text-sm text-muted-foreground">{activity.category}</p>
            </CardContent>
          </Card>
        ))}
      </div>
    </div>
  );
};

export default App;
