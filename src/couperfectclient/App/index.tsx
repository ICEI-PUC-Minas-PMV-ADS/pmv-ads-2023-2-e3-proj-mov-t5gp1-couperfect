import React from 'react';

import { NavigationContainer } from '@react-navigation/native';
import { AuthProvider } from './Context/auth';
import Routes from './Routes';

function App(): JSX.Element {
  return (
    <NavigationContainer>
      <AuthProvider key={null} type={''} props={undefined}>
        <Routes />
      </AuthProvider>
    </NavigationContainer>
  );
}

export default App;
