import { createNativeStackNavigator } from '@react-navigation/native-stack';
import SignInPage from '../Pages/SignInPage';
import SplashPage from '../Pages/SplashPage';
import HomePage from '../Pages/HomePage';

const {Screen, Navigator} = createNativeStackNavigator();

const AnonRoutes = () => {
    return (
        <Navigator screenOptions={{ headerShown: false }}>
            <Screen name="SplashPage" component={SplashPage} />
            <Screen name="HomePage" component={HomePage} />
            <Screen name="SignInPage" component={SignInPage} />
        </Navigator>
    )
}

export default AnonRoutes;