import { createNativeStackNavigator } from '@react-navigation/native-stack';
import StartPage from '../Pages/StartPage';
import SignInPage from '../Pages/SignInPage';

const {Screen, Navigator} = createNativeStackNavigator();

const AnonRoutes = () => {
    return (
        <Navigator screenOptions={{ headerShown: false }}>
            <Screen name="Start" component={StartPage} />
            <Screen name="SignInPage" component={SignInPage} />
        </Navigator>
    )
}

export default AnonRoutes;