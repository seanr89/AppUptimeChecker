// Connections/sqlParam.js
'use strict';

class SqlParam{
  /**
   *
   * @param {*} name
   * @param {*} type
   * @param {*} value
   */
  constructor(name, type, value){
    this.name = name;
    this.type = type;
    this.value = value;
  }
}

module.exports = SqlParam;
