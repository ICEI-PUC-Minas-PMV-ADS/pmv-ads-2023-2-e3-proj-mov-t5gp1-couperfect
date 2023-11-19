import { createNativeStackNavigator } from '@react-navigation/native-stack';
import SignInPage from '../Pages/SignInPage';
const {Screen, Navigator} = createNativeStackNavigator();

const AnonRoutes = () => {
    return (
        <Navigator screenOptions={{ headerShown: false }}>
            <Screen name="SingInPage" component={SignInPage} />
        </Navigator>
    )
}

export default AnonRoutes;