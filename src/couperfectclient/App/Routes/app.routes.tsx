import {createNativeStackNavigator} from '@react-navigation/native-stack';
import HomePage from '../Pages/HomePage';
import MainMenuPage from '../Pages/MainMenuPage';

const {Screen, Navigator} = createNativeStackNavigator();

const AppRoutes = () => {
  return (
    <Navigator>
      <Screen name="MainMenu" component={MainMenuPage} />
      <Screen name="HomePage" component={HomePage} />
    </Navigator>
  );
};

export default AppRoutes;
