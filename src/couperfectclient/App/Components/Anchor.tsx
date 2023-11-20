import { CommonActions, useNavigation } from '@react-navigation/native';
import { Pressable } from 'react-native';
import ListItem from './ListItem';

interface AnchorProps {
  route: string;
  text: string;
}

const Anchor: React.FC<AnchorProps> = ({route, text}) => {
  const navigate = useNavigation();

  function handleNavigate() {
    navigate.dispatch(CommonActions.navigate(route));
  }

  return (
    <Pressable onPress={handleNavigate}>
      <ListItem text={text} />
    </Pressable>
  );
};

export default Anchor;
