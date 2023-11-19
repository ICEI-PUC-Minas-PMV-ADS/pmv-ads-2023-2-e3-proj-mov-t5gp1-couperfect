import React from 'react';
import { Text, TouchableOpacity, StyleSheet } from 'react-native';

interface ButtonProps {
  title: string;
  onPress: () => void;
}

const TextButton: React.FC<ButtonProps> = (props) => {
  return (
    <TouchableOpacity onPress={props.onPress}>
      <Text style={styles.buttonText}>{props.title}</Text>
    </TouchableOpacity>
  );
}

const styles = StyleSheet.create({
  buttonText: {
    color: '#DAD8D8',
    fontSize: 20,
    textShadowColor: '#0149BF',
    textShadowOffset: {width: -1, height: 1},
    textShadowRadius: 10
  },
});

export default TextButton;
