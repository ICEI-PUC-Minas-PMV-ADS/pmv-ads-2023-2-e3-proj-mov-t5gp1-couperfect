import { StackActions, useNavigation } from "@react-navigation/native";
import React from "react";
import { Button, Text, View } from "react-native";

const HomePage: React.FC = () => {
    const navigation = useNavigation();
    const pushAction = StackActions.push('MainMenu');
    
    const onPress = () => {
        navigation.dispatch(pushAction)
    }

    return (
        <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
            <Text>Home Page</Text>
            <Button onPress={onPress} title="Entrar" />
        </View>
    );
}

export default HomePage;