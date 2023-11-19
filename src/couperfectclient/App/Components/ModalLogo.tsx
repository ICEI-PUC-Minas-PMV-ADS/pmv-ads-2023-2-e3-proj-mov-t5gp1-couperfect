import { useNavigation } from '@react-navigation/native';
import { ReactNode } from 'react';
import { StyleSheet, View } from 'react-native';
import CoupLogoSvg from '../Assets/Logo.svg';
import colors from '../Styles';
import IconButton from './IconButton';

interface ModalLogoProps {
  children: ReactNode;
}

const ModalLogo: React.FC<ModalLogoProps> = ({ children }) => {
  const navigate = useNavigation();

  return (
    <View style={styles.container}>
      <IconButton
        style={styles.iconButtonPosition}
        type='back'
        onPress={() => navigate.goBack()} />
      <View style={{
        alignItems: 'center',
        width: '50%'
      }}>
        <CoupLogoSvg />
      </View>
      <View style={styles.verticleLine} />
      <View style={{ width: '50%', padding: 30 }}>
        {children}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    flexDirection: 'row',
    width: '80%',
    height: '80%',
    backgroundColor: colors.Neutral,
    borderRadius: 4
  },
  verticleLine: {
    height: '90%',
    width: 1,
    backgroundColor: '#909090',
  },
  iconButtonPosition: {
    position: 'absolute',
    top: 10,
    left: 10
  }
});

export default ModalLogo;