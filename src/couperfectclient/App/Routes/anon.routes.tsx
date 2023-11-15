import { createNativeStackNavigator } from '@react-navigation/native-stack';
import SignInPage from '../Pages/SignInPage';

const {Screen, Navigator} = createNativeStackNavigator();

const AnonRoutes = () => {
    return (
        <Navigator>
            <Screen name="HomePage" component={SignInPage} />
        </Navigator>
    )
}

export default AnonRoutes;