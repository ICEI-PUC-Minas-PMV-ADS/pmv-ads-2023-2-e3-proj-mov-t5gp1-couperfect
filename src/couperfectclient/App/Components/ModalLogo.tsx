import {ReactNode} from 'react';
import {StyleSheet, View} from 'react-native';
import BackButton from './BackButton';

interface ModalLogoProps {
  children: ReactNode;
}

const ModalLogo: React.FC<ModalLogoProps> = ({children}) => {
  return (
    <View style={style.container}>
      <BackButton />
      {children}
    </View>
  );
};

const style = StyleSheet.create({
    container: {
        justifyContent: 'space-around',
        alignItems: 'center',
        flexDirection: 'row',
        width: '70%',
        height: '80%',
        position: 'relative'
    },
});

export default ModalLogo;