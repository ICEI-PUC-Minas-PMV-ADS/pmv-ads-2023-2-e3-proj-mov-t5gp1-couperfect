import { useNavigation } from "@react-navigation/native";
import React from "react";
import {ImageBackground, Text, View, StyleSheet, Pressable } from "react-native";
import CoupLogo from '../Assets/Logo.svg'
import { CommonActions } from "@react-navigation/native";


const MainMenuPage: React.FC = () => {
    const navigator = useNavigation();

    function buscarSalaPress() 
    {
        navigator.dispatch(CommonActions.navigate('dtgfdg'))
    }
    function criarSalaPress()
    {
        navigator.dispatch(CommonActions.navigate(''))
    }
    function sairPress()
    {
        navigator.dispatch(CommonActions.navigate('HomePage'))
    }

    return (
        <ImageBackground
          source={require('../Assets/Background.png')}
          style={styles.background}>
          <CoupLogo style={styles.logoConteiner}/>
          <Pressable onPress={buscarSalaPress}>
            <Text style = {styles.textStyle}>
                Buscar Sala
            </Text>
          </Pressable>
          <Pressable onPress={criarSalaPress}>
            <Text style = {styles.textStyle}>
                Criar Sala
            </Text>
          </Pressable>
          <Pressable onPress={sairPress}>
            <Text style = {styles.textStyle}>
                Sair
            </Text>
          </Pressable>
        </ImageBackground>
    );
}
const styles = StyleSheet.create({
    background: {
      width: '100%',
      height: '100%',
      justifyContent: 'center',
    },
    logoConteiner : {
        alignSelf:'center',
        marginTop:'5%',
        marginLeft:'50%',
    },
    textStyle:{
        color: 'white',
        paddingBottom:5,
        paddingRight:30,
        paddingLeft:40,
        fontSize: 30
    }
  });

export default MainMenuPage;