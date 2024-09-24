"use client";
import { Row, Col } from "react-bootstrap";
import testData from "../../data/easier.json";
import { useEffect, useState } from "react";
import styles from "./quiz.module.css";
import {
  findNode,
  Node,
  NodeType,
  updateAnswers,
} from "./../../quiz-logic/quiz-functions";
// import CalculatorComponent from "@/components/Calculator";

export default function QuizPage() {
  const [currentNode, setCurrentNode] = useState<Node>(testData);
  const [answers, setAnswers] = useState<string[]>([]);
  const [tip, setTip] = useState<number>(0);

  //Runs everytime currentJson changes
  useEffect(() => {
    setAnswers(updateAnswers(currentNode));

    if (
      currentNode.nodeType === NodeType.Link &&
      currentNode.connect_id !== undefined
    ) {
      const newCurrentNode: Node = findNode(testData, currentNode.connect_id);

      setCurrentNode(newCurrentNode);
    }
  }, [currentNode]);

  // Runs everytime a answer is pressed
  function nextAnswer(answer: string) {
    // Update json to where the answer leads
    if (currentNode.answers?.[answer] === undefined) {
      return;
    }
    setCurrentNode(currentNode.answers[answer]);
  }

  //Move to the next tooltip
  function nextTip(currentTip: number) {
    setTip(currentTip + 1);
  }

  return (
    <>
      <Row className="text-center justify-content-lg-center">
        <Col xxl={1} xl={0}></Col>

        <Col xxl={6} xl={6} lg={12}>
          <div className="blue_bubble">
            {tip === 0 ? (
              <h3>
                {currentNode?.question}
                {currentNode?.answer}
                {currentNode?.link !== undefined ? (
                  <a href={currentNode.link.link}>{currentNode.link.text}</a>
                ) : (
                  <></>
                )}
              </h3>
            ) : (
              <h3>
                {currentNode?.moreInfo?.[tip - 1]}
              </h3>
            )}

            <button
              className={styles["grey-button"]}
              onClick={() => nextTip(tip)}
            >
              <h3>Next</h3>
            </button>

          </div>
          <div className={styles["button-container"]}>
            {answers.map((answer, index) => (
              <button
                className={styles["grey-button"]}
                key={index}
                onClick={() => nextAnswer(answer)}
              >
                <h3>{answer}</h3>
              </button>
            ))}
          </div>
          {/* <CalculatorComponent /> */}
        </Col>

        <Col xxl={5} xl={6} lg={8} md={12} className="overlayContainer">
          <img
            src="/images/Fin.png"
            alt="Logo"
            className="overlayImage"
          />
        </Col>
      </Row>
    </>
  );
}
