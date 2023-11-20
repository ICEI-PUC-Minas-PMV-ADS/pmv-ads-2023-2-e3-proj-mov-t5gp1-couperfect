import {createNativeStackNavigator} from '@react-navigation/native-stack';
import MainMenuPage from '../Pages/MainMenuPage';
import QueryRoomsPage from '../Pages/QueryRooms';
import CreateRoom from '../Pages/CreateRoom';

const {Screen, Navigator} = createNativeStackNavigator();

const AppRoutes = () => {
  return (
    <Navigator screenOptions={{ headerShown: false }}>
      <Screen name="MainMenuPage" component={MainMenuPage} />
      <Screen name="QueryRooms" component={QueryRoomsPage} />
      <Screen name="CreateRoom" component={CreateRoom} />
    </Navigator>
  );
};

export default AppRoutes;
