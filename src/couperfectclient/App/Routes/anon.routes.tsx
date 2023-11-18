import { createNativeStackNavigator } from '@react-navigation/native-stack';
import SignInPage from '../Pages/SignInPage';
import SignUpPage from '../Pages/SignUpPage';

const {Screen, Navigator} = createNativeStackNavigator();

const AnonRoutes = () => {
    return (
        <Navigator screenOptions={{ headerShown: false }}>
            <Screen name="SingInPage" component={SignInPage} />
            <Screen name="SignUpPage" component={SignUpPage} />
        </Navigator>
    )
}

export default AnonRoutes;