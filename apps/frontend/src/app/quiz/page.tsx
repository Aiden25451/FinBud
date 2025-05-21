import QuizPage from '@/_components/Quiz-Component/quiz';
import ResponsiveImage from '@/_components/Responsive-Image-Component/ResponsiveImage';
import { getHistoryFunction } from '@/_utils/quiz-functions';
import { getSession } from '@auth0/nextjs-auth0';

export default async function Page() {

  //TODO Refactor getHistoryFunction() to handle both routes instead of an if option

  const data = await getHistoryFunction();
  return <QuizPage data={data}></QuizPage>;

  // data={{
  //   loading: false,
  //   historyArray: [0],
  //   error: 'Not logged in',
  //   initialState: false,
  // }}
}
