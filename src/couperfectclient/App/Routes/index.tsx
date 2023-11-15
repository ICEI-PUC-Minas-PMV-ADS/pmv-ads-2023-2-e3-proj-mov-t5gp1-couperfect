import React, {useContext} from 'react';
import AnonRoutes from './anon.routes';
import AuthContext from '../Context/auth';
import AppRoutes from './app.routes';

const Routes: React.FC = () => {
  const {signed} = useContext(AuthContext);
  return signed ? <AppRoutes /> : <AnonRoutes />;
};

export default Routes;
