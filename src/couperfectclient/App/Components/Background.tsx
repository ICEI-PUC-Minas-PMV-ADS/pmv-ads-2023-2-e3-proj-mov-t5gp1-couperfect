import React, {ReactNode} from 'react';
import {ImageBackground, StyleSheet, ViewStyle} from 'react-native';

const Background: React.FC<{children: ReactNode; style?: ViewStyle}> = ({
  children,
  style,
}) => {
  const inputStyle = StyleSheet.compose(defaultStyle, style);

  return (
    <ImageBackground
      style={inputStyle}
      source={require('../Assets/Background.png')}>
      {children}
    </ImageBackground>
  );
};

const defaultStyle: ViewStyle = {
  width: '100%',
  height: '100%',
  justifyContent: 'center',
  alignItems: 'center',
};

export default Background;
