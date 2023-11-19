import {createNativeStackNavigator} from '@react-navigation/native-stack';
import MainMenuPage from '../Pages/MainMenuPage';

const {Screen, Navigator} = createNativeStackNavigator();

const AppRoutes = () => {
  return (
    <Navigator screenOptions={{ headerShown: false }}>
      <Screen name="MainMenuPage" component={MainMenuPage} />
    </Navigator>
  );
};

export default AppRoutes;
