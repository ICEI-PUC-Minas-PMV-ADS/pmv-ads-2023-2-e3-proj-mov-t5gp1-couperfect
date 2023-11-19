import React, { ReactNode } from 'react';
import { ImageBackground, StyleSheet } from 'react-native';

const Background: React.FC<{ children: ReactNode }> = ({ children }) => {
  return (
    <ImageBackground style={styles.background} source={require('../Assets/Background.png')}>
      {children}
    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  background: {
    width: '100%',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default Background;